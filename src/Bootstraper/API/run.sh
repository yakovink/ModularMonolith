dotnet clean
dotnet build
dotnet run
kill $(
    ps -a |
    grep dotnet | 
    awk '{print $1}'
    )
echo "API has been stopped" || echo "API is not running"
