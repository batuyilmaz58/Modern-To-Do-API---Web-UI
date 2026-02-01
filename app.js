const BASE_URL = "https://localhost:7212/api/Todo";

// 1. LİSTELEME
async function getTodos() {
    try {
        const response = await fetch(`${BASE_URL}/getalltodo`);
        const result = await response.json();
        
        const listElement = document.getElementById("todoList");
        listElement.innerHTML = ""; 

        // Görüntüde paylaştığın "Entities" büyük harf yapısına göre:
        const items = result.Entities || result.entities;

        if (items) {
            items.forEach(todo => {
                const li = document.createElement("li");
                const currentId = todo.id || todo.Id;
                const currentTitle = todo.title || todo.Title;

                li.innerHTML = `
                    <span>${currentTitle}</span>
                    <div class="actions">
                        <i class="fas fa-edit" title="Düzenle" onclick="editTodo(${currentId}, '${currentTitle}')"></i>
                        <i class="fas fa-trash" title="Sil" onclick="deleteTodo(${currentId})"></i>
                    </div>
                `;
                listElement.appendChild(li);
            });
        }
    } catch (error) {
        console.error("Hata:", error);
    }
}

// 2. EKLEME
async function addTodo() {
    const input = document.getElementById("todoInput");
    if (!input.value) return;

    try {
        await fetch(`${BASE_URL}/addtodo`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ title: input.value })
        });
        input.value = "";
        getTodos();
    } catch (error) {
        console.error("Ekleme hatası:", error);
    }
}

// 3. SİLME (Senin Controller: deletetodo/{id})
async function deleteTodo(id) {
    if(!confirm("Bu görevi silmek istediğine emin misin?")) return;

    try {
        await fetch(`${BASE_URL}/deletetodo/${id}`, { method: "DELETE" });
        getTodos();
    } catch (error) {
        console.error("Silme hatası:", error);
    }
}

// 4. GÜNCELLEME (Senin Controller: updatetodo/{id})
async function editTodo(id, oldTitle) {
    const newTitle = prompt("Görevi güncelle:", oldTitle);
    if (!newTitle || newTitle === oldTitle) return;

    try {
        await fetch(`${BASE_URL}/updatetodo/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ title: newTitle })
        });
        getTodos();
    } catch (error) {
        console.error("Güncelleme hatası:", error);
    }
}

// İlk yüklemede verileri çek
getTodos();