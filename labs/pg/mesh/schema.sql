CREATE SCHEMA IF NOT EXISTS mesh;

\ir id/id.sql
\ir id/id_sequence.sql
\ir id/id_new.sql

\ir http/http_get.sql

\ir mess/xslt_match.sql

\ir node/node.sql
\ir node/node_xslt_transform.sql
\ir node/node_web_publish.sql
\ir node/data.sql

\ir mess/session_get.sql
\ir mess/session_set.sql

\ir mess/node_view.sql
\ir mess/node_view_change.sql
\ir mess/node_view_change_trigger.sql
\ir mess/node_fts.sql
\ir mess/node_dump_from.sql
\ir mess/node_dump_to.sql
\ir mess/node_review.sql

\ir mess/balance_list.sql
\ir mess/balance_total.sql
\ir mess/balance_invoice.sql

\ir mess/mess_anki_list.sql
\ir mess/mess_balance_list.sql
\ir mess/mess_burpee1.sql
\ir mess/mess_burpee_stats.sql
\ir mess/mess_deadline_list.sql
\ir mess/mess_depends_list.sql
\ir mess/mess_drink.sql
\ir mess/mess_drink_goals.sql
\ir mess/mess_drink_goals_v1.sql
\ir mess/mess_gmg_done.sql
\ir mess/mess_gmg_done_days.sql
\ir mess/mess_gmg_invoice.sql
\ir mess/mess_location_list.sql
\ir mess/mess_rank_list.sql
\ir mess/mess_scales_stats.sql
\ir mess/mess_scheme_tags.sql
\ir mess/mess_tags.sql
\ir mess/mess_tags_list.sql
\ir mess/mess_todo.sql
\ir mess/mess_q_view.sql

\ir mess/mess_goals.sql
\ir mess/mess_goals_on_update.sql
