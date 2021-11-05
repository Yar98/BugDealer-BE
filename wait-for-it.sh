#!/bin/bash

set -e


>&2 echo "SQL Server is starting up"
sleep 20

>&2 echo "SQL Server is up - executing command"
exec "$@"