:connect $database.server$
use [$database.name$];
go

declare @Result tinyint;
$securables:{securable |
select @Result=HAS_PERMS_BY_NAME('$securable.name$', '$securable.type$', '$securable.permission$')

if (@Result=1)
begin
	print '  Success: the permission $securable.permission$ was granted on $securable.type$::$securable.name$ for principal $principal$'
end
else
begin
	print '  Failure: the permission $securable.permission$ was NOT granted on $securable.type$::$securable.name$ for principal $principal$'
end

}$
	
go

