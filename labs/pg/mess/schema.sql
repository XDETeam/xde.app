CREATE INDEX
	mess_idx_content 
ON
	mesh.mess
USING
	GIN ((content::text) gin_trgm_ops)
;

\ir mess_scheme_tags.sql

\ir mess_anki_list.sql
\ir mess_balance_list.sql
\ir mess_deadline_list.sql
\ir mess_depends_list.sql
\ir mess_rank_list.sql
\ir mess_drink_goals.sql

\ir mess_gmg_done.sql
\ir mess_gmg_done_days.sql
\ir mess_gmg_invoice.sql

\ir mess_goals.sql
\ir mess_goals_on_update.sql

\ir mess_xslt_match.sql

\ir mess_dump_from.sql
\ir mess_dump_to.sql

\ir mess_fts.sql
