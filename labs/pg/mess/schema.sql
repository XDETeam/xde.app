CREATE INDEX
	mess_idx_content 
ON
	mess.mess
USING
	GIN ((content::text) gin_trgm_ops)
;
