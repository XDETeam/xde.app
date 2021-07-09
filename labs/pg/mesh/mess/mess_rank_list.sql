CREATE OR REPLACE VIEW mesh.mess_rank_list
AS SELECT
	rank.index,
	mess.id,
	rank.title,
	mess.content
FROM
	mess.mess,
	LATERAL XMLTABLE(
		('//rank'::text) PASSING (mess.content) COLUMNS
		index integer PATH ('@index'::text),
		title text PATH ('../@title'::text)
	) rank
ORDER BY
	rank.index
;
