CREATE VIEW mesh.mess_deadline_list
AS SELECT
	deadline.at,
	mess.id,
	deadline.title,
	mess.content
FROM
	mesh.mess,
	LATERAL XMLTABLE(
		'//deadline'::text PASSING (mess.content)
		COLUMNS
			at timestamp without time zone PATH ('@at'::text),
			title text PATH ('../@title'::text)
	) deadline
ORDER BY
	deadline.at
;
