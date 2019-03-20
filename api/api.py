from flask import Flask
import jdatabase
import datetime


app = Flask(__name__)
app.config["SECRET_KEY"] = "secret"


db = jdatabase.Jdatabase(host="alpha.joshwidrick.com", user="mmouser", passwd="8KYyLzM729wqdfyk", db="mmo")
db.create_table_if_false_check("users",
                               {"username": ["VARCHAR(128)", "PRIMARY KEY"],
                                "join_date": ["DATETIME(0)", "NOT NULL"]})
db.create_table_if_false_check("user_coins",
                               {"username": ["VARCHAR(128)", "PRIMARY KEY"],
                                "coins": ["INT(0)", "NOT NULL"]})
db.create_table_if_false_check("user_deaths",
                               {"username": ["VARCHAR(128)", "PRIMARY KEY"],
                                "deaths": ["INT(0)", "NOT NULL"]})


@app.route("/add_user/<username>")
def add_user(username):
    try:
        current = db.get_one("users", where=("username=%s", [username]))[1]
    except Exception:
        current = None
    if current is not None:
        return f"User already exists. Added at {current}"
    else:
        db.insert("users", {"username": username, "join_date": datetime.datetime.utcnow()})
        return "User added."


@app.route("/coins/<username>/<process>/<amount>")
def coins(username, process, amount):
    try:
        current_coins = int(db.get_one("user_coins", where=("username=%s", [username]))[1])
    except Exception:
        current_coins = 0
    if process == "add":
        current_coins += int(amount)
    elif process == "sub":
        current_coins -= int(amount)
    else:
        return "failed"
    db.insert_or_update("user_coins", {"username": username, "coins": int(current_coins)}, "username")
    return str(current_coins)


@app.route("/death/<username>")
def death(username):
    try:
        current_deaths = int(db.get_one("user_deaths", where=("username=%s", [username]))[1])
    except Exception:
        current_deaths = 0
    current_deaths += 1
    db.insert_or_update("user_deaths", {"username": username, "deaths": int(current_deaths)}, "username")
    return str(current_deaths)


if __name__ == "__main__":
    app.run(debug=True)
