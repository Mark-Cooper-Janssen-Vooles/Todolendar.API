module.exports = {
  apps : [{
    name   : "Todolendar API",
    script : "sudo /home/ec2-user/dotnet/dotnet Todolendar.API.dll --urls \"http://*:5000;https://*5001\"",
    cwd: "/home/ec2-user/api/Todolendar.API/publish",
    watch: true,
    max_memory_testart: "2G"
  }]
}
