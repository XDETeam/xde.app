DO $VERSION$ BEGIN
	IF env.is_clean() THEN
		DROP TABLE IF EXISTS mesh.node;
		
		DROP DOMAIN IF EXISTS mesh.id CASCADE;
		DROP SEQUENCE IF EXISTS mesh.id_sequence CASCADE;
	END IF;
END $VERSION$;
