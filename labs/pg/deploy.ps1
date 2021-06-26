Clear-Host
$env:LC_MESSAGES='en_US.UTF-8'
$env:PGCLIENTENCODING='utf-8'

Push-Location -Path $PSScriptRoot

# $deploy = 'deploy.sql'
$deploy = 'deploy-simple.sql'

psql `
	--set=ENV_DEBUG=1 `
	--set=ENV_CLEAN=1 `
	--set=ENV_SHARD=1 `
	--set=ENV_EPOCH='''2010-01-01''' `
	--username=postgres `
	--dbname=xde `
	--file=$deploy `
	--host=192.168.1.2 `
	--quiet `
	--set ON_ERROR_STOP=on

Write-Host Done...

Pop-Location