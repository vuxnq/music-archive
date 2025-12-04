# Run the SQL script
sqlite3 musicarchive.db < scripts/create_tables.sql

# TODO:
# add nullable description to all tables
# add nullable cover/image Path/Url for artist and release
# add user - string username, string password (no need for encrypting)

# more complex:
# rating for release INT 0 - 10 - this would require M:N relation user:release
# M:N genres release

# not related to db that much:
# users will be able to create lists - list of releases <- organizing
# comment sections
