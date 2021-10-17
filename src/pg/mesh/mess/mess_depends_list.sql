CREATE OR REPLACE VIEW mesh.mess_depends_list
AS SELECT
	depends."on" AS id,
	(SELECT content FROM mess.mess WHERE id=depends."on") AS content,
	count(*) AS count
FROM
	mess.mess,
	LATERAL XMLTABLE(('//depends'::text) PASSING (mess.content) COLUMNS
		"on" ltree PATH ('@on'::text),
		title text PATH ('../title'::text)
	) depends
GROUP BY
	depends."on"
ORDER BY
	count(*) DESC
;
