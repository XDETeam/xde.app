/***************************************************************************************************

	Upgrade environment version
	
	@param version
	Version to set

 ***************************************************************************************************/
CREATE OR REPLACE PROCEDURE env.version_upgrade(version integer)
AS $$
	INSERT INTO env.version (
		id
	) SELECT
		version_upgrade.version
	WHERE
		env.version_less(version_upgrade.version)
	;
$$ LANGUAGE sql;
