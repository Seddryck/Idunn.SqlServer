:connect $database.server$
use [$database.name$];
go

declare @Result tinyint;
select @Result=HAS_PERMS_BY_NAME('$principal$', 'USER', 'IMPERSONATE')

if (@Result<>1)
begin
	print '  Inconclusive: the user ' + CURRENT_USER + ' cannot impersonate $principal$'
end
else
BEGIN
	@Result=0
	begin try
		execute as user='$principal$';
		set @Result=1;
	end TRY
    begin catch
		if (error_number()=9048)
		begin
			print '  Inconclusive: the user $principal$ doesn''t exit or cannot log to the database $database.name$ on $database.server$';
		end
		else
		begin
			print '  Inconclusive: Error = ' + ERROR_MESSAGE(); 
		end
	end catch
	
	if (@Result=1)
	begin
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

		revert;

		}$
	end
end
go

