CREATE OR REPLACE FUNCTION mesh.node_review(
    _filter text = NULL,
    _operator text = '@'
)
RETURNS SETOF mess.mesh
AS $$
    SELECT
		url,
		content
    FROM
		mess.mesh
    WHERE 
		NOT xmlexists('/story' PASSING BY VALUE "content")
		AND (
			_filter IS NULL
       		OR (
				CASE _operator
               		WHEN '~' THEN url ~ _filter::lquery
               		WHEN '@' THEN url @ _filter::ltxtquery
               		ELSE url = _filter::ltree
        		END)
		)
    ORDER BY
        -- TODO: Perf
        random()
    LIMIT 1;
$$ LANGUAGE sql;
