module.exports = {
  apps : [{
    name   : "Todolendar API",
    exec_mode: "cluster",
    env: {
      NODE_ENV: "production"
    },
    script : "sudo /home/ec2-user/dotnet/dotnet Todolendar.API.dll --urls \"http://*:5000;https://*5001\"",
    cwd: "/home/ec2-user/api/Todolendar.API/publish",
    watch: true,
    max_memory_testart: "2G"
  }]
}

// use "pm2 monit" to monitor 
// use "pm2 logs" to see the logs, only shows 15 lines
// cd /home/ec2-user/.pm2/logs 
// cat Todolendar-API-error.log => to see all logs 

// to end a process on a port, first display all: sudo lsof -i -P -n | grep LISTEN
// the 2nd column is the PID. then use sudo kill <PID>

// to kill all on port 5000: sudo kill -9 $(sudo lsof -t -i:5000)