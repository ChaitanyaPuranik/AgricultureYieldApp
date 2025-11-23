from fastapi import FastAPI
from pydantic import BaseModel
import joblib
import pandas as pd

app = FastAPI()

# Load trained model pipeline
model = joblib.load("yield_model.pkl")

# Define input schema
class YieldInput(BaseModel):
    Region: str
    Soil_Type: str
    Crop: str
    Rainfall_mm: int
    Temperature_Celsius: int
    Fertilizer_Used: bool
    Irrigation_Used: bool
    Weather_Condition: str
    Days_to_Harvest: int

@app.post("/predict")
def predict_yield(data: YieldInput):
    df = pd.DataFrame([data.dict()])
    pred = model.predict(df)[0]
    return {"predicted_yield": float(pred)}
