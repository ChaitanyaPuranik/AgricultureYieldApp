import pandas as pd
import joblib
from sklearn.model_selection import train_test_split
from sklearn.compose import ColumnTransformer
from sklearn.preprocessing import OneHotEncoder
from sklearn.pipeline import Pipeline
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error, r2_score
import numpy as np

def load_data(path="../data/crop_yield.csv"):
    df = pd.read_csv(path)
    return df

def get_preprocessor(categorical_cols, numeric_cols):
    return ColumnTransformer(
        transformers=[
            ("cat", OneHotEncoder(handle_unknown="ignore"), categorical_cols),
            ("num", "passthrough", numeric_cols)
        ]
    )

def train():
    print("[INFO] Loading dataset...")
    df = load_data()

    # Define target + features
    y = df["Yield_tons_per_hectare"]
    X = df.drop("Yield_tons_per_hectare", axis=1)

    categorical_cols = ["Region", "Soil_Type", "Crop", "Weather_Condition"]
    numeric_cols = [
        "Rainfall_mm",
        "Temperature_Celsius",
        "Fertilizer_Used",
        "Irrigation_Used",
        "Days_to_Harvest"
    ]

    # Build preprocessing pipeline
    preprocessor = get_preprocessor(categorical_cols, numeric_cols)

    # Full model pipeline
    model = Pipeline(steps=[
        ("preprocessor", preprocessor),
        ("regressor", LinearRegression())
    ])

    # Train-test split
    X_train, X_test, y_train, y_test = train_test_split(
        X, y, test_size=0.2, random_state=42
    )

    print("[INFO] Training model...")
    model.fit(X_train, y_train)

    # Evaluate
    y_pred = model.predict(X_test)
    mse = mean_squared_error(y_test, y_pred)
    rmse = np.sqrt(mse)
    r2 = r2_score(y_test, y_pred)

    print("=====================================")
    print("Training Complete!")
    print(f"MSE  : {mse:.4f}")
    print(f"RMSE : {rmse:.4f}")
    print(f"R2   : {r2:.4f}")
    print("=====================================")

    # Save model
    joblib.dump(model, "yield_model.pkl")
    print("[INFO] Model saved as yield_model.pkl")

if __name__ == "__main__":
    train()
