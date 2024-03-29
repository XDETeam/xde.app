/*TODO:
-- Revoking from public
REVOKE ALL ON DATABASE xde FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON ALL TABLES IN SCHEMA public from PUBLIC;
ALTER DEFAULT PRIVILEGES FOR ROLE CURRENT_USER REVOKE ALL ON FUNCTIONS FROM PUBLIC;

-- Admin group
DO $$ BEGIN
IF EXISTS (SELECT FROM pg_roles WHERE rolname = 'admins') THEN
    ALTER DEFAULT PRIVILEGES IN SCHEMA public, mess, mesh REVOKE ALL PRIVILEGES ON TABLES FROM admins;
    ALTER DEFAULT PRIVILEGES IN SCHEMA public, mess, mesh REVOKE USAGE ON SEQUENCES FROM admins;
    REVOKE ALL PRIVILEGES ON ALL TABLES IN SCHEMA public, mess, mesh FROM admins;
    REVOKE ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public, mess, mesh FROM admins;
    REVOKE ALL PRIVILEGES ON ALL FUNCTIONS IN SCHEMA public, mess, mesh FROM admins;
    REVOKE ALL ON DATABASE xde FROM admins;
    REVOKE ALL ON SCHEMA public, mess, mesh FROM admins;
    DROP ROLE IF EXISTS admins;
END IF;
END $$;

CREATE ROLE admins NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;
GRANT ALL ON DATABASE xde TO admins;
GRANT ALL ON SCHEMA public, mess, mesh TO admins;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public, mess, mesh TO admins;
GRANT USAGE ON ALL SEQUENCES IN SCHEMA public, mess, mesh TO admins;
ALTER DEFAULT PRIVILEGES IN SCHEMA public, mess, mesh GRANT ALL PRIVILEGES ON TABLES TO admins;
ALTER DEFAULT PRIVILEGES IN SCHEMA public, mess, mesh GRANT USAGE ON SEQUENCES TO admins;
ALTER DEFAULT PRIVILEGES IN SCHEMA public, mess, mesh GRANT ALL PRIVILEGES ON FUNCTIONS TO admins;
GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public, mess, mesh TO admins;

GRANT admins TO yavulan;
*/