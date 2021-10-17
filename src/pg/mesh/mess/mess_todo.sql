CREATE VIEW mesh.mess_todo
AS
WITH RECURSIVE cte AS (
    SELECT 
		(xpath('name(/*)'::text, mess.content))[1]::text AS node,
		mess.id::text AS id,
		(xpath('/*/@title'::text, mess.content))[1]::text AS title,
		NULL::timestamp without time zone AS deadline,
		0 AS level,
		mess.content
    FROM
		mess.mess
    WHERE
		mess.id = ANY(ARRAY ['stan'::ltree, 'stan.gym.drawing'::ltree, 'stan.money.postbank'::ltree, 'stan.money.transferwise'::ltree])
    
	UNION ALL SELECT
		(xpath('name(/*)'::text, children.content))[1]::text AS node,
		(cte_1.id || '.'::text) || COALESCE(children.id, (children.node || '#'::text) || children.index::text) AS id,
		COALESCE(children.title, regexp_replace(children.text::text, '^\s+|\s+$'::text, ''::text, 'g'::text)) AS title,
		(
			SELECT
				LEAST(min("xmltable".at), cte_1.deadline) AS "least"
            FROM
				XMLTABLE(('/*/deadline'::text)
					PASSING (children.content)
                    COLUMNS at timestamp without time zone PATH ('@at'::text)
				)
		) AS deadline,
		cte_1.level + 1 AS level,
        children.content
    FROM
		cte cte_1,
		LATERAL XMLTABLE(('/*/*'::text)
			PASSING (cte_1.content)
			COLUMNS
				content xml PATH ('.'::text),
				node text PATH ('name(.)'::text),
				id text PATH ('@id'::text),
				text xml PATH ('text()'::text),
				title text PATH ('@title'::text),
				index integer PATH ('count(./preceding-sibling::*)+1'::text)
			) children
)
SELECT
	cte.node,
	cte.id,
	cte.title,
	cte.deadline,
	cte.level,
	cte.content
FROM
	cte
;
