Write-Host "🧼 Cleaning all obj directories..."
Get-ChildItem -Recurse -Directory -Filter obj | ForEach-Object {
    Remove-Item $_.FullName -Recurse -Force -ErrorAction SilentlyContinue
}

Write-Host "🧼 Cleaning all files inside bin directories (keeping folders)..."
Get-ChildItem -Recurse -Directory -Filter bin | ForEach-Object {
    Get-ChildItem $_.FullName -Recurse -File | Remove-Item -Force -ErrorAction SilentlyContinue
}

Write-Host "✅ Clean complete."
