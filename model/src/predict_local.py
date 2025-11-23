# predict_local.py
import joblib
from utils import prepare_input

def predict():
    model = joblib.load("yield_model.pkl")
    print("[INFO] Model loaded.")

    sample = prepare_input(
        Region="North",
        Soil_Type="Loam",
        Crop="Wheat",
        Rainfall_mm=300,
        Temperature_Celsius=22,
        Fertilizer_Used=1,
        Irrigation_Used=1,
        Weather_Condition="Sunny",
        Days_to_Harvest=120
    )

    prediction = model.predict(sample)[0]
    print(f"Predicted Yield: {prediction:.2f} tons/hectare")

if __name__ == "__main__":
    predict()
