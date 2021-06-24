CREATE OR REPLACE PROCEDURE mess.session_set(
    _sid text
)
AS $$
    SELECT set_config('mess.sid', _sid, false);
$$ LANGUAGE sql;
