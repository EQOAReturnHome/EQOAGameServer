function event_say()
diagOptions = {}
    npcDialogue = "Fayspires was once a college for the arcane arts, before the burning of the Elddar Forest. When the elves fled, we came here and claimed it as a refuge. This became our sanctuary, and then our home. Many of the elves were however inclined to immerse themselves in nature, as they had known long ago in the Elddar Forest. Those elves chose to build a new home just southwest of here, across the lake known as the city of Tethelin. Though we are separate cities, we maintain a unified force for the survival of our kind."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end