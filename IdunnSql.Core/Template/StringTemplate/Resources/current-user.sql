$if (sqlcmd)$
:connect $database.server$
use [$database.name$];
go
$endif$

declare @Result tinyint;
$securables:{securable |
select  @Result=HAS_PERMS_BY_NAME('$securable.name$', '$securable.type$', '$securable.permission$')
select '$database.server$', '$database.name$','$securable.name$', '$securable.type$', '$securable.permission$', CURRENT_USER, @Result;

if (@Result=1)
begin
	print '  Success: the permission $securable.permission$ was granted on $securable.type$::$securable.name$ for principal ' + CURRENT_USER
end
else
begin
	print '  Failure: the permission $securable.permission$ was NOT granted on $securable.type$::$securable.name$ for principal ' + CURRENT_USER
end

}$
$if (sqlcmd)$
go
$endif$
