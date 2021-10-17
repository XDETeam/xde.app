/***************************************************************************************************

	Check that current environment is less than selected version
	
	@param version
	Version to check the environment for
	
	@returns
	Returns true if current environment is less than selected version and false otherwise

 ***************************************************************************************************/
CREATE OR REPLACE FUNCTION env.version_less(version integer)
RETURNS boolean
AS $$
	SELECT
		COALESCE(MAX(version.id), 0) < version_less.version
	FROM
		env.version
	;
$$ LANGUAGE sql;
