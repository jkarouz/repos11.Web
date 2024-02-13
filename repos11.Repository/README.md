# Web Repository
Database repository

## Run on Package Manager Console CLI migrations:
```sh
Update-Database
```

Package Manager Console CLI migrations:
## Running the app
```sh
# Sample Add migration
$ Add-Migration InitialCreate
$ Add-Migration AlterMasterUsersAddress

# Sample replace existing migration
$ Add-Migration AlterMasterUsersAddress -Force

# Sample apply migration to database
$ Update-Database

# Sample apply migration to database (can see SQL)
$ Update-Database –Verbose

# Sample apply migration to database (specific version)
$ Update-Database –TargetMigration: AlterMasterUsersAddress

# Sample apply migration to database (roll back)
$Update-Database –TargetMigration: AddMasterUsers

# Sample generate script SQL
$ Update-Database -Script -SourceMigration: $InitialDatabase -TargetMigration: AlterMasterUsersAddress
```