$dll = 'C:\Users\\Administrator\\.nuget\\packages\\wolverinefx\\5.19.1\\lib\\net10.0\\Wolverine.dll'
$a = [Reflection.Assembly]::LoadFrom($dll)
try {
    $types = $a.GetTypes() | Where-Object { $_.Name -eq 'ISender' }
    foreach ($t in $types) {
        Write-Host "Type: $($t.FullName)"
        $t.GetMethods() | Select-Object Name, @{Name = 'ReturnType'; Expression = { $_.ReturnType.Name } }, @{Name = 'Params'; Expression = { $_.GetParameters() | ForEach-Object { $_.ParameterType.Name + ' ' + $_.Name } } } | Format-Table -AutoSize
    }
}
catch [Reflection.ReflectionTypeLoadException] {
    Write-Host 'Failed to load types due to loader exceptions:'
    foreach ($ex in $_.Exception.LoaderExceptions) {
        Write-Host "- $($ex.Message)"
    }
}
catch {
    Write-Host "Unexpected error: $($_.Exception.Message)"
}