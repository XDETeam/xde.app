create view mess_balance_list as
SELECT mess.id,
       - debit.amount,
       debit.currency
FROM mess.mess,
     LATERAL XMLTABLE(('//debit'::text) PASSING (mess.content)
                      COLUMNS amount numeric PATH ('@amount'::text), currency text PATH ('@currency'::text)) debit
UNION ALL
SELECT mess.id,
       credit.amount AS "?column?",
       credit.currency
FROM mess.mess,
     LATERAL XMLTABLE(('//credit'::text) PASSING (mess.content)
                      COLUMNS amount numeric PATH ('@amount'::text), currency text PATH ('@currency'::text)) credit;

alter table mess_balance_list
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_balance_list to yavulan;

