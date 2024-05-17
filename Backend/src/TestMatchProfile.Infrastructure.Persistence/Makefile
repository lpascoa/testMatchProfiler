ROOT_DIR=$(shell pwd)

.PHONY: scaffold-db
scaffold-db:
	dotnet ef dbcontext scaffold "Server=tcp:localhost,1433;Database=TestMatchProfiler;User ID=sa;Password=Test!123;TrustServerCertificate=true;Trusted_Connection=False;Encrypt=True;" Microsoft.EntityFrameworkCore.SqlServer -o DB -f -c ApplicationDbContext -d
