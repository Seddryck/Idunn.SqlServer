$principals:{principal |
$principal.databases:{database |
:connect $database.server$
use [$database.name$];
go

$impersonate(principal.name, database, database.securables)$

go
}$
}$