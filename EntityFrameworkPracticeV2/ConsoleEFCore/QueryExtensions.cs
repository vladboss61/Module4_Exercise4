﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ConsoleEFCore
{
    public static class QueryExtensions
    {
        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            using var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var relationalCommandCache = enumerator.Private("_relationalCommandCache");
            var selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
            var factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);

            string sql = command.CommandText;
            return sql;
        }

        private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);

        private static T Private<T>(this object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);

        public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            this IQueryable<TOuter> outer,
            IQueryable<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {

            // generic methods
            var selectManies = typeof(Queryable).GetMethods()
                .Where(x => x.Name == "SelectMany" && x.GetParameters().Length == 3)
                .OrderBy(x => x.ToString().Length)
                .ToList();
            var selectMany = selectManies.First();
            var select = typeof(Queryable).GetMethods().First(x => x.Name == "Select" && x.GetParameters().Length == 2);
            var where = typeof(Queryable).GetMethods().First(x => x.Name == "Where" && x.GetParameters().Length == 2);
            var groupJoin = typeof(Queryable).GetMethods().First(x => x.Name == "GroupJoin" && x.GetParameters().Length == 5);
            var defaultIfEmpty = typeof(Queryable).GetMethods().First(x => x.Name == "DefaultIfEmpty" && x.GetParameters().Length == 1);

            // need anonymous type here or let's use Tuple
            // prepares for:
            // var q2 = Queryable.GroupJoin(db.A, db.B, a => a.Id, b => b.IdA, (a, b) => new { a, groupB = b.DefaultIfEmpty() });
            var tuple = typeof(Tuple<,>).MakeGenericType(
                typeof(TOuter),
                typeof(IQueryable<>).MakeGenericType(
                    typeof(TInner)
                    )
                );
            var paramOuter = Expression.Parameter(typeof(TOuter));
            var paramInner = Expression.Parameter(typeof(IEnumerable<TInner>));
            var groupJoinExpression = Expression.Call(
                null,
                groupJoin.MakeGenericMethod(typeof(TOuter), typeof(TInner), typeof(TKey), tuple),
                new Expression[]
                    {
                    Expression.Constant(outer),
                    Expression.Constant(inner),
                    outerKeySelector,
                    innerKeySelector,
                    Expression.Lambda(
                        Expression.New(
                            tuple.GetConstructor(tuple.GetGenericArguments()),
                            new Expression[]
                                {
                                    paramOuter,
                                    Expression.Call(
                                        null,
                                        defaultIfEmpty.MakeGenericMethod(typeof (TInner)),
                                        new Expression[]
                                            {
                                                Expression.Convert(paramInner, typeof (IQueryable<TInner>))
                                            }
                                )
                                },
                            tuple.GetProperties()
                            ),
                        new[] {paramOuter, paramInner}
                )
                    }
                );

            // prepares for:
            // var q3 = Queryable.SelectMany(q2, x => x.groupB, (a, b) => new { a.a, b });
            var tuple2 = typeof(Tuple<,>).MakeGenericType(typeof(TOuter), typeof(TInner));
            var paramTuple2 = Expression.Parameter(tuple);
            var paramInner2 = Expression.Parameter(typeof(TInner));
            var paramGroup = Expression.Parameter(tuple);
            var selectMany1Result = Expression.Call(
                null,
                selectMany.MakeGenericMethod(tuple, typeof(TInner), tuple2),
                new Expression[]
                    {
                    groupJoinExpression,
                    Expression.Lambda(
                        Expression.Convert(Expression.MakeMemberAccess(paramGroup, tuple.GetProperty("Item2")),
                                           typeof (IEnumerable<TInner>)),
                        paramGroup
                ),
                    Expression.Lambda(
                        Expression.New(
                            tuple2.GetConstructor(tuple2.GetGenericArguments()),
                            new Expression[]
                                {
                                    Expression.MakeMemberAccess(paramTuple2, paramTuple2.Type.GetProperty("Item1")),
                                    paramInner2
                                },
                            tuple2.GetProperties()
                            ),
                        new[]
                            {
                                paramTuple2,
                                paramInner2
                            }
                )
                    }
                );

            // prepares for final step, combine all expressinos together and invoke:
            // var q4 = Queryable.SelectMany(db.A, a => q3.Where(x => x.a == a).Select(x => x.b), (a, b) => new { a, b });
            var paramTuple3 = Expression.Parameter(tuple2);
            var paramTuple4 = Expression.Parameter(tuple2);
            var paramOuter3 = Expression.Parameter(typeof(TOuter));
            var selectManyResult2 = selectMany
                .MakeGenericMethod(
                    typeof(TOuter),
                    typeof(TInner),
                    typeof(TResult)
                )
                .Invoke(
                    null,
                    new object[]
                        {
                        outer,
                        Expression.Lambda(
                            Expression.Convert(
                                Expression.Call(
                                    null,
                                    select.MakeGenericMethod(tuple2, typeof(TInner)),
                                    new Expression[]
                                        {
                                            Expression.Call(
                                                null,
                                                where.MakeGenericMethod(tuple2),
                                                new Expression[]
                                                    {
                                                        selectMany1Result,
                                                        Expression.Lambda(
                                                            Expression.Equal(
                                                                paramOuter3,
                                                                Expression.MakeMemberAccess(paramTuple4, paramTuple4.Type.GetProperty("Item1"))
                                                            ),
                                                            paramTuple4
                                                        )
                                                    }
                                            ),
                                            Expression.Lambda(
                                                Expression.MakeMemberAccess(paramTuple3, paramTuple3.Type.GetProperty("Item2")),
                                                paramTuple3
                                            )
                                        }
                                ),
                                typeof(IEnumerable<TInner>)
                            ),
                            paramOuter3
                        ),
                        resultSelector
                        }
                );

            return (IQueryable<TResult>)selectManyResult2;
        }
    }
}
