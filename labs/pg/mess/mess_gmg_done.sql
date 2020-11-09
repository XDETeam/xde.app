CREATE VIEW mesh.mess_gmg_done
AS
SELECT
	done.at,
	done."time",
	done.task
FROM
	mesh.mess,
	LATERAL XMLTABLE(('//done'::text) PASSING (mess.content) COLUMNS
		at date PATH ('@at'::text),
		"time" integer PATH ('@time'::text),
		task text PATH ('text()'::text),
	    paid date PATH ('../@paid'::text)
	) done
WHERE
	mess.id = 'gmg.invoice'::ltree
    AND done.paid IS NULL
ORDER BY
	done.at DESC
;
