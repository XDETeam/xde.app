CREATE OR REPLACE VIEW mesh.balance_list
AS SELECT
    credit.on,
    trim(BOTH E' \n\r\t' FROM title) as title,
    credit.unit,
    credit.lot,
    credit.cost,
    COALESCE(value, cost * lot) as value
FROM
    mesh.node_view,
    LATERAL XMLTABLE(('//credit'::text) PASSING (node_view.content) COLUMNS
        "to" ltree PATH('@to'),
        "on" date PATH('@on'),
        "cost" numeric PATH('@cost'),
        "unit" text PATH('@unit'),
        "lot" numeric PATH('@lot'),
        "value" numeric PATH('@value'),
        "title" text PATH('text()')
	) credit

UNION ALL SELECT
    debit.on,
    trim(BOTH E' \n\r\t' FROM title) as title,
    debit.unit,
    debit.lot,
    debit.cost,
    -COALESCE(value, cost * lot) as value
FROM
    mesh.node_view,
    LATERAL XMLTABLE(('//debit'::text) PASSING (node_view.content) COLUMNS
        "to" ltree PATH('@to'),
        "on" date PATH('@on'),
        "cost" numeric PATH('@cost'),
        "unit" text PATH('@unit'),
        "lot" numeric PATH('@lot'),
        "value" numeric PATH('@value'),
        "title" text PATH('text()')
	) debit
;