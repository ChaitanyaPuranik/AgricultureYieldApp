# utils.py
import pandas as pd

def prepare_input(
    Region,
    Soil_Type,
    Crop,
    Rainfall_mm,
    Temperature_Celsius,
    Fertilizer_Used,
    Irrigation_Used,
    Weather_Condition,
    Days_to_Harvest
):
    return pd.DataFrame([{
        "Region": Region,
        "Soil_Type": Soil_Type,
        "Crop": Crop,
        "Rainfall_mm": Rainfall_mm,
        "Temperature_Celsius": Temperature_Celsius,
        "Fertilizer_Used": Fertilizer_Used,
        "Irrigation_Used": Irrigation_Used,
        "Weather_Condition": Weather_Condition,
        "Days_to_Harvest": Days_to_Harvest
    }])
