$principals:{principal |
$principal.databases:{database |
/***********************************************/
/*  Checks permission for $database.server$\\$database.name$ */
/***********************************************/
:connect $database.server$
use [$database.name$];
go

$impersonate(principal=principal.name, securables=database.securables)$

go

}$
}$