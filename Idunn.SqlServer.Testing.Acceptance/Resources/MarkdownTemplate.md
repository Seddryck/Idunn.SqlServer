$principals:{principal |
# $principal.name$
$principal.databases:{database |
## $database.server$

### $database.name$

|     object     |      type      |   permission   |
|----------------|----------------|----------------|
$database.securables:{securable |
| $securable.name$ | $securable.type$ | $securable.permission$ |  
}$
}$
}$

