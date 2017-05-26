$account = "<account>"
$path = "<path>"
$permission = [system.Security.AccessControl.FileSystemRights]::<permission>

Write-Verbose "Checking permission '$permission' on '$path' for user or group '$account'"

#Check if the ALC is directly available for this user
$hasDirectPermission = Get-Acl -Path $path | % {$_.Access} | where {$_.IdentityReference -eq $account -and $_.AccessControlType -eq "Allow" -and ($permission -band $_.FileSystemRights -ne 0)} | % {$_.Count -ge 1}

#Check for each groups if the members contain the account
If (-not $hasDirectPermission)
{
    $hasIndirectPermission = 0
    $accountName = $account.ToString().split('\')[1]
    $groups = Get-Acl -Path $path | % {$_.Access} | where {$_.AccessControlType -eq "Allow" -and ($permission -band $_.FileSystemRights -ne 0)} | % {$_.IdentityReference}
    
    $groups | ForEach-Object {       
        if (-not $hasIndirectPermission)
        {
            #Local group 
            $localName = $_.ToString().split('\')[1]
            $localGroup = [ADSI]"WinNT://./$localName,group"
            try {
                $members = $localGroup.psbase.Invoke("Members")
            } catch {
                if ($_.Exception.InnerException.HResult -ne -2147022676) { # It's a user not a group
                    Write-Warning $_
                }
            }
            $members | ForEach-Object {
                $memberName = $_.GetType().InvokeMember("Name", 'GetProperty', $null, $_, $null)
                $hasIndirectPermission = $hasIndirectPermission -or ($memberName -eq $accountName)
            }
        }
        if (-not $hasIndirectPermission)
        {
            #AD group 
            try {
                if (Get-Command Get-ADGroupMember -errorAction SilentlyContinue) {
                    $members = (Get-ADGroupMember -Identity $group -Recursive | % {$_.Name})
                    $hasIndirectPermission = $hasIndirectPermission -or ($members -contains $user)
                }     
            } catch {
                Write-Warning $_
            }
        }
    }
}
$hasPermission = $hasDirectPermission -or $hasIndirectPermission
Write-Output $hasPermission