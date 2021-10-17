CREATE OR REPLACE VIEW mesh.mess_balance_list AS
SELECT
    mess.id,
    -debit.amount,
    debit.currency
FROM
    mess.mess,
	LATERAL XMLTABLE(
		('//debit'::text) PASSING (mess.content) COLUMNS
		amount decimal PATH ('@amount'::text),
		currency text PATH ('@currency'::text)
	) debit
UNION ALL SELECT
    mess.id,
    credit.amount,
    credit.currency
FROM
    mess.mess,
    LATERAL XMLTABLE(
        ('//credit'::text) PASSING (mess.content) COLUMNS
         amount decimal PATH ('@amount'::text),
         currency text PATH ('@currency'::text)
     ) credit
;
