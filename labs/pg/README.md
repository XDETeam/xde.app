## Version file template

```
DO $VERSION$ BEGIN
	IF env.is_clean() THEN
		DROP SCHEMA IF EXISTS schema_name CASCADE;
		...
	END IF;
END $VERSION$;

\ir schema_name/schema.sql
...
```