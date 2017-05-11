$principals:{principal |
$principal.databases:{database |
:connect $database.server$
use [$database.name$];
go

$current_user(principal=principal.name, database=database, securables=database.securables)$

go
}$
}$
