

1- migration olusturma 
Dbcontext in oldugu projede
    dotnet ef migrations add Users -s ../Vb.Api/ 


2- migration degisikliklerini db ye gecme yansitma guncelle migrate etme
Olusan migrationlarin calistirilmasi 
sln dizininde 
    dotnet ef database update --project "./Vb.Data" --startup-project "./Vb.Api"




--ortak proje yapisinda 
dotnet ef migrations add UniqueMigrationName
dotnet ef database update