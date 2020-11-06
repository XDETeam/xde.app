CREATE VIEW mesh.mess_gmg_done
AS SELECT
	done.at,
	done."time",
	done.task
FROM
	mesh.mess,
	LATERAL XMLTABLE(('//done'::text) PASSING (mess.content) COLUMNS
		at date PATH ('@at'::text),
		"time" integer PATH ('@time'::text),
		task text PATH ('text()'::text)
	) done
WHERE
	mess.id = 'gmg.invoice'::ltree
ORDER BY
	done.at DESC
;
