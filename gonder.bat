dotnet publish -c Release -o deploy

scp -r ./deploy admin@127.0.0.1:/var/www/helloapp/

ssh admin@127.0.0.1 'sudo systemctl restart dotnetapp.service'
