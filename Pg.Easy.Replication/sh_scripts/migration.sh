#!/usr/bin/env bash
dotnet ef migrations add Init --project .. --startup-project ..
dotnet ef database update --connection "Host=localhost;Port=5433;Database=db1;Username=user1;Password=pass1;" --project .. --startup-project ..