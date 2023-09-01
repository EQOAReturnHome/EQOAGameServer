-- coachman galdah

local coaches = require("Scripts/ports")

local playerCoaches = {
    qeynos_coach = "Get me a horse to Qeynos.",
    forkwatch_coach = "Get me a horse to Forkwatch."
}

local dialogueOptions = {}
 
function  event_say(choice)
        if (GetPlayerFlags(mySession, "highbourne_coach") == "true") then
        if (choice:find("Qeynos")) then
            TeleportPlayer(
                mySession,
                GetWorld(coaches.qeynos.world),
                coaches.qeynos.x,
                coaches.qeynos.y,
                coaches.qeynos.z,
                coaches.qeynos.facing
            )
        elseif (choice:find("Forkwatch")) then
            TeleportPlayer(
                mySession,
                GetWorld(coaches.forkwatch.world),
                coaches.forkwatch.x,
                coaches.forkwatch.y,
                coaches.forkwatch.z,
                coaches.forkwatch.facing
            )
        else
            npcDialogue = "Where would you like to go?"
            for coach, diag in pairs(playerCoaches) do
                if ((GetPlayerFlags(mySession, "admin") or GetPlayerFlags(mySession, coach)) == "true") then
                    table.insert(dialogueOptions, diag)
                end
            end
            SendDialogue(mySession, npcDialogue, dialogueOptions, thisEntity.CharName)
        end
    else
        if (choice:find("Yes")) then
            npcDialogue = "Excellent, you can now use this coach any time."
            SetPlayerFlags(mySession, "highbourne_coach", "true")
            SendDialogue(mySession, npcDialogue, dialogueOptions, thisEntity.CharName)
        elseif (choice:find("No")) then
            npcDialogue = "If you aren't interested then why are you wasting my time."
            SendDialogue(mySession, npcDialogue, dialogueOptions, thisEntity.CharName)
        else
            npcDialogue = "Would you like to sign the coachman's ledger?"
            dialogueOptions = {"Yes", "No"}
            SendDialogue(mySession, npcDialogue, dialogueOptions, thisEntity.CharName)
        end
    end
end
