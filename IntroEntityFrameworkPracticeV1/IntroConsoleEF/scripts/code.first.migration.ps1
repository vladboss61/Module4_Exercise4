dotnet ef migrations add InitCreateDatabase #--context ApplicationContext # if you have several context
dotnet ef migrations script 0 InitCreateDatabase # from - to
dotnet ef migrations list