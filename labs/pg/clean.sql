DO $VERSION$ BEGIN
	IF env.is_clean() THEN
		DROP TABLE IF EXISTS mesh.node;
	END IF;
END $VERSION$;