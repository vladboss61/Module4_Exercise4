
#dotnet ef migrations --help
#dotnet ef database --help
#dotnet ef migrations list

#dotnet ef migrations add Name_Of_Migration # add migration but not apply (update) to the database
#dotnet ef migrations remove # remove last migration in the migration list

#Apply/Update the database.

#dotnet ef database update Name_Of_Migration_1 # Name_Of_Migration_1
#dotnet ef database update Name_Of_Migration_2 # Name_Of_Migration_2
#dotnet ef database update Name_Of_Migration_3 # Name_Of_Migration_3
#dotnet ef database update Name_Of_Migration_2 # Name_Of_Migration_2

#dotnet ef database drop # drop database
#dotnet ef migrations script 0 Name_Of_Migration # (--from 0 --to Name_Of_Migration) Display the executable SQL script

dotnet ef database update #--connection "Data Source=localhost;Initial Catalog = ConsoleApplication;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"