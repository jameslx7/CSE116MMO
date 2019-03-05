from flask import Flask
import jdatabase


app = Flask(__name__)
app.config["SECRET_KEY"] = "secret"


db = jdatabase.Jdatabase(host="alpha.joshwidrick.com", user="mmouser", passwd="8KYyLzM729wqdfyk", db="mmo")
db.create_table_if_false_check("user_coins",
                               {"username": ["VARCHAR(128)", "PRIMARY KEY"],
                                "coins": ["INT(0)", "NOT NULL"]})


@app.route("/<username>/<process>/<amount>")
def test(username, process, amount):
    current_coins = int(db.get_one("user_coins", where=("username=%s", username))[1])
    print(current_coins)
    if current_coins is None:
        current_coins = 0
    if process == "add":
        current_coins += int(amount)
    elif process == "sub":
        current_coins -= int(amount)
    else:
        return "failed"
    db.insert_or_update("user_coins", {"username": username, "coins": int(current_coins)}, where=("username=%s", username))
    return str(current_coins)


if __name__ == "__main__":
    app.run(debug=True)
