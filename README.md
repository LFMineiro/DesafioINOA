# 📈 Desafio INOA  

A aplicação utiliza a API pública **Brapi** para obter os preços dos ativos da B3 e enviar alertas por e-mail quando determinados valores são atingidos.  

---

## 🛠 Instalação  

No diretório raiz do projeto, crie o arquivo `appsettings.json` com suas credenciais de e-mail e o token da API Brapi:  

```json
{
    "Api": {
        "Token": "seu_token_da_api",
        "Delay": 60000
    },
    "Smtp": {
        "Server": "smtp.gmail.com",
        "Port": 587,
        "Username": "seu_email@gmail.com",
        "Password": "sua_senha"
    },
    "Sender": "seu_email@gmail.com",
    "Recipients": [
        "destinatario1@gmail.com",
        "destinatario2@gmail.com"
    ]
}
````
--------

## ⚙️ Como Usar
------
O projeto pode ser chamado via linha de comando da forma:

````
DesafioINOA.exe <TICKER> <PREÇO_DE_VENDA> <PREÇO_DE_COMPRA>
````

📌 Exemplo de Uso
````
DesafioINOA.exe PETR4 22.67 22.59
````


