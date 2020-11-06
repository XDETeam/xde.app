\set dev_version 1

SET CLIENT_MIN_MESSAGES TO WARNING;

CREATE OR REPLACE FUNCTION _version()
RETURNS int
AS $$ BEGIN
	RETURN (SELECT MAX(version.id) FROM env.version);
EXCEPTION
	WHEN
		invalid_schema_name OR undefined_table
	THEN
		RETURN 0
	;
END $$ LANGUAGE plpgsql;

select _version() as version, _version() < 1 or :dev_version = 1 as upgrade \gset
\if :upgrade
	\echo Version 0.00.0001

	\ir 0.00.0001.sql
	
	CALL env.version_upgrade(1);
\endif

-- select _version() as version, _version() < 2 or :dev_version = 2 as upgrade \gset
-- \if :upgrade
--	\echo Version 0.00.0002
	
	-- \ir 0.00.0002.sql
	
	-- CALL env.version_upgrade(2);
-- \endif

DROP FUNCTION IF EXISTS _version();
