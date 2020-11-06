\if :ENV_CLEAN
	DROP SCHEMA IF EXISTS env CASCADE;

	DROP TABLE IF EXISTS mesh.node CASCADE;
	
	DROP DOMAIN IF EXISTS mesh.id CASCADE;
	DROP SEQUENCE IF EXISTS mesh.id_sequence CASCADE;
	
	DROP VIEW IF EXISTS mesh.mess_goals;
	DROP VIEW IF EXISTS mesh.mess_deadline_list;
	DROP VIEW IF EXISTS mesh.mess_rank_list;
	DROP VIEW IF EXISTS mesh.mess_depends_list;
	DROP VIEW IF EXISTS mesh.mess_drink_goals;
\endif

\ir env/schema.sql
\ir mesh/schema.sql
\ir mess/schema.sql
