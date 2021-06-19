CREATE OR REPLACE FUNCTION mess.node_review(
    _filter text = NULL,
    _operator text = '@'
)
RETURNS SETOF mess.mesh
AS $$
    SELECT
            url,
           xt.content
    FROM mess.mesh,
         XMLTABLE('//*[not(ancestor::story)]' PASSING content COLUMNS "content" XML PATH '.') xt
    WHERE _filter IS NULL
       OR (CASE _operator
               WHEN '~' THEN url ~ _filter::lquery
               WHEN '@' THEN url @ _filter::ltxtquery
               ELSE url = _filter::ltree
        END)
    ORDER BY
        -- TODO: Perf
        random()
    LIMIT 1;
$$ LANGUAGE sql
SECURITY DEFINER; --TODO:Deal with security issues
