using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayFive : MonoBehaviour
{
    public List<long> seedIds = new List<long>();

    public TextAsset puzzleInput;

    // Start is called before the first frame update
    void Start()
    {
        List<string> lines = puzzleInput.text.Split('\n').ToList();

        string seedIdsString = lines[0].Remove(0, 7);
        string[] seedIdsArray = seedIdsString.Split(' ');

        foreach (var seedId in seedIdsArray)
        {
            if (seedId != "")
            {
                seedIds.Add(long.Parse(seedId));
            }
        }

        List<Map> maps = new List<Map>();
        Map seedToSoilMap = new Map();
        Map soilToFertilizerMap = new Map();
        Map fertilizerToWaterMap = new Map();
        Map waterToLightMap = new Map();
        Map lightToTemperatureMap = new Map();
        Map temperatureToHumidityMap = new Map();
        Map humidityToLocationMap = new Map();

        maps.Add(seedToSoilMap);
        maps.Add(soilToFertilizerMap);
        maps.Add(fertilizerToWaterMap);
        maps.Add(waterToLightMap);
        maps.Add(lightToTemperatureMap);
        maps.Add(temperatureToHumidityMap);
        maps.Add(humidityToLocationMap);

        int currentMap = -1;


        lines.RemoveAt(0);
        //remove all empty lines
        lines.RemoveAll(string.IsNullOrEmpty);


        foreach (var line in lines)
        {
            if (char.IsLetter(line[0]))
            {
                currentMap++;
            }
            else
            {
                MapLine newMapLine = new MapLine();
                //split line by spaces
                string[] splitLine = line.Split(' ');
                //remove empty entries
                splitLine = splitLine.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                //first number is destination range start
                newMapLine.destinationRangeStart = long.Parse(splitLine[0]);
                //second number is destination range length
                newMapLine.destinationRangeLength = long.Parse(splitLine[1]);
                //third number is source range start
                newMapLine.sourceRangeStart = long.Parse(splitLine[2]);
                
                maps[currentMap].mapLines.Add(newMapLine);
                
                
            }
        }


        foreach (var seedID in seedIds)
        {
            var malleableSeedID = seedID;
            foreach (var map in maps)
            {
                foreach (var mapLine in map.mapLines)
                {
                  
                    if (malleableSeedID >= mapLine.sourceRangeStart && malleableSeedID < mapLine.sourceRangeStart + mapLine.destinationRangeLength)
                    {
                        malleableSeedID = mapLine.destinationRangeStart + (malleableSeedID - mapLine.sourceRangeStart);
                    }
                   
                    
                }
            }
            
            Debug.Log(malleableSeedID);
        }
        Debug.Log(maps.Count);


        //try parse every first character, if it's not a number
    }

    // Update is called once per frame
    void Update()
    {
    }


    public class Map
    {
        //constructor that creates list
        public Map()
        {
            mapLines = new List<MapLine>();
        }

        public List<MapLine> mapLines = new List<MapLine>();
    }

    public class MapLine
    {
        public long destinationRangeStart;
        public long destinationRangeLength;
        public long sourceRangeStart;
    }
}