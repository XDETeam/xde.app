\if :ENV_CLEAN
	DROP SCHEMA IF EXISTS env CASCADE;

	DROP TABLE IF EXISTS mesh.node;
	
	DROP DOMAIN IF EXISTS mesh.id CASCADE;
	DROP SEQUENCE IF EXISTS mesh.id_sequence CASCADE;
\endif

\ir env/schema.sql
\ir mesh/schema.sql
