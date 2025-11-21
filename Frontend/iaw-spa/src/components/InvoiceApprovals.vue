<template>
    <v-container class="pa-4" fluid>
        <v-row class="mb-12 mt-4" dense justify="center">
            <v-col cols="12" md="6">
            <v-card
                class="py-4"
                rounded="lg"
            >
                <template #title>
                <h2 class="text-h5 font-medium text-center">
                    Invoice Approval Form
                </h2>
                </template>
                <v-card-text>
                    <v-form ref="form" v-model="valid"  @submit.prevent>
                    <v-row>
                    <v-col cols="12" md="12">
                        <v-col cols="12" md="12">
                        <v-number-input :rules="amountRules" control-variant="hidden" v-model="formData.amount" :precision="2" variant="outlined" hide-details="auto"  :min="1" label="Amount"></v-number-input>
                        </v-col>

                        <v-col cols="12" md="12">
                            <v-checkbox
                                v-model="formData.isPreferredVendor"
                                color="primary"
                                label="Is Preferred Vendor"
                                hide-details
                            ></v-checkbox>
                        </v-col>
                    </v-col>
                    </v-row>
                    </v-form>
                </v-card-text>
                <v-card-subtitle v-if="canShowList"  class="ma-4">
                    <v-row>
                        <v-col cols="12" md="6">
                            <h4>Required approvers:</h4>
                            <v-chip v-if="approvals.length > 0" v-for="approval in approvals" class="ma-2" variant="elevated" color="primary">
                                {{ approval }}
                            </v-chip>
                            <v-chip v-else class="ma-2" variant="outlined" color="grey">
                                No approvals required.
                            </v-chip>
                        </v-col>
                    </v-row>
                    <v-row>                        
                        <v-treeview
                            v-model:opened="open"
                            :items="items"
                            density="compact"
                            item-value="title"
                            activatable
                            open-on-click
                        >
                            <template v-slot:prepend="{ item, isOpen }">
                            <v-icon  :icon="isOpen ? 'mdi-account-arrow-up' : 'mdi-account'"></v-icon>

                            </template>
                        </v-treeview>
                    </v-row>
                </v-card-subtitle>
                <v-card-actions class="ma-4" >
                    <v-btn text="Reset" :loading="loading" variant="tonal" @click="reset"></v-btn>

                    <v-spacer></v-spacer>

                    <v-btn text="Send"  :loading="loading"  type="submit" color="green" variant="elevated" @click="send"></v-btn>
                </v-card-actions>
            </v-card>
            </v-col>
        </v-row>    
    </v-container>
</template>
<script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue';

const formData = ref({
  amount: null,
  isPreferredVendor: false
});
const amountRules = ref([
  (v: number) => !!v || 'Amount is required',
  (v: number) => (v && v > 0) || 'Amount must be greater than zero',
]); 
const valid = ref(false);
const loading = ref(false);
const approvals = ref<string[]>([]);
const canShowList = ref(false);
const form = ref();
const open = ref<string[]>([]);
const items = ref([
    {
        title: 'Amount under $1,000 and Not Preferred Vendor',
        children: [
            { title: 'Manager', file: 'pdf' }
        ],
    },
    {
        title: 'Amount under $1,000 and Preferred Vendor',
        children: [
            { title: 'No approvals required', file: 'png' }
        ],
    },
    {
        title: 'Amount $1,000 - $9,999 and Not Preferred Vendor',
        children: [
            { title: '(Manager + Director)', file: 'png' }
        ],
    },
    {
        title: 'Amount $1,000 - $9,999 and Preferred Vendor',
        children: [
            { title: '(Director)', file: 'png' }
        ],
    },
    {
        title: 'Amount $10,000 and above and Not Preferred Vendor',
        children: [
            { title: '(Manager + Director + VP)', file: 'pdf' }
        ],
    },
    {
        title: 'Amount $10,000 and above and Preferred Vendor',
        children: [
            { title: '(Director + VP)', file: 'pdf' }
        ],
    },
]);


async function send() {
    const result = await form.value.validate()
    valid.value = result.valid;
    if (!valid.value) return;
    
    try {
        loading.value = true;
        const resultData = await axios.post('https://localhost:5000/api/invoices/determine-approvers', formData.value);
        if (resultData.data.isSuccess){
            canShowList.value = true;
            approvals.value = resultData.data.data.requiredApprovers;
        } else {
            console.error('Error creating post:', resultData.data.message);
        }
    } catch (error) {
        console.error('Error sending form data:', error);
    } finally {
        loading.value = false;
    }
}

function reset() {
    approvals.value = [];
    canShowList.value = false;
    form.value.reset();
    formData.value = {
        amount: null,
        isPreferredVendor: false
    };
}
</script>